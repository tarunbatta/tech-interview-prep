(function () {
    'use strict';

    const app = {};

    const puppeteer = require('puppeteer');
    const cheerio = require('cheerio');
    const fs = require('fs');
    const http = require('http');
    const sanitizeHtml = require('sanitize-html');

    const config = require('./config');
    const helper = require('./helper');

    let browser = null;
    let page = null;

    let problems = [];

    const init = async () => {
        browser = await puppeteer.launch({
            headless: false,
            slowMo: 500,
        });
        page = await browser.newPage();
        await page.setViewport({
            width: 1024,
            height: 768,
            deviceScaleFactor: 1,
        });

        await getProblems();
        await page.waitFor(config.timeout);
        if (config.enableScraping) {
            await scrapeData();
            await page.waitFor(config.timeout);
        }
        await generateCodeFiles();

        await browser.close();
    };

    const getProblems = async () => {
        const metadata = await helper.readFile(config.metadataFilePath);

        if (metadata == null || metadata.length == 0 || config.isRefetchMetadata) {
            await page.goto(config.problemsUrl, config.pageNavigationOptions);
            await page.waitFor(config.timeout);

            const option = (await page.$x(
                '//select[@class="form-control"]/option[text() = "all"]'
            ))[0];
            const value = await (await option.getProperty('value')).jsonValue();
            await page.select('select.form-control', value);
            await page.waitFor(config.timeout);

            const bodyHTML = await page.evaluate(() => document.body.innerHTML);
            const $ = cheerio.load(bodyHTML);

            $('div.question-list-table > table > tbody.reactable-data > tr').each(
                (i, element) => {
                    problems.push({
                        number: $(element).find('td:nth-child(2)').text(),
                        title: $(element).find('td:nth-child(3) > div > a').text(),
                        url: config.siteUrl + $(element).find('td:nth-child(3) > div > a').attr('href'),
                        difficulty: $(element).find('td:nth-child(6) > span').text(),
                        question: '',
                        code: '',
                    });
                }
            );

            config.writeFile(config.metadataFilePath, JSON.stringify(problems));
            console.log('Metadata written to file');
        } else {
            problems = JSON.parse(metadata);
            console.log('Metadata loaded from file');
        }

        for (let i = 0; i < problems.length; i++) {
            const filePath = config.problemsFolderPath + problems[i].number + '.json';
            const isFilePresent = await config.isFilePresent(filePath);

            if (!isFilePresent) {
                config.writeFile(filePath, JSON.stringify(problems[i]));
            }
        }

        console.log('Json files craetes for problems');
    };

    const scrapeData = async () => {
        let isLanguageSelected = false;

        for (let i = 0; i < problems.length; i++) {
            let problem = problems[i];
            const filePath = config.problemsFolderPath + problem.number + '.json';
            const isFilePresent = await config.isFilePresent(filePath);

            if (isFilePresent) {
                const fileData = await helper.readFile(filePath);
                problem = JSON.parse(fileData);
            }

            if (problem.question == null || problem.question == '' || config.isRefetchProblems) {
                await page.goto(problem.url, config.pageNavigationOptions);

                const bodyHTML = await page.evaluate(() => document.body.innerHTML);
                const $ = cheerio.load(bodyHTML);
                await page.waitFor(config.timeout * 2);

                if (!isLanguageSelected) {
                    await page.click('div.ant-select-selection');
                    await page.waitFor(config.timeout);
                    await page.click('li[data-cy="' + config.selectedlanguage + '"]');
                    await page.waitFor(config.timeout);
                    isLanguageSelected = true;
                }

                problem.question = $('div.question-content__JfgR > div').html();

                if (problem.question != null && problem.question.length > 0) {
                    problem.question = sanitizeHtml(problem.question, { allowedTags: [], allowedAttributes: {} });
                }

                $('div.CodeMirror-code div').each(function (index, element) {
                    problem.code += $(element).find('pre').text();
                });

                await helper.deleteFile(filePath);
                await page.waitFor(config.timeout);
                config.writeFile(filePath, JSON.stringify(problem));
                await page.waitFor(config.timeout);
            }
        }

        console.log('Json files craete for problems after parsing');
    };

    const generateCodeFiles = async () => {
        await helper.deleteContentsOfFolder(config.codeFolderPath);

        for (let i = 0; i < problems.length; i++) {
            let problem = problems[i];
            const filePath = config.problemsFolderPath + problems[i].number + '.json';
            const isFilePresent = await config.isFilePresent(filePath);

            if (isFilePresent) {
                let problemFile = '';
                const newLine = '\n';
                const fileData = await helper.readFile(filePath);
                problem = JSON.parse(fileData);

                problemFile += '// Url:' + problem.url + newLine + newLine;
                problemFile += '/*' + newLine;
                problemFile += problem.number + '. ' + problem.title + newLine;
                problemFile += problem.difficulty + newLine + newLine;

                if (problem.question == null || problem.question.length == 0) {
                    problemFile += '// {{ MISSING QUESTION }}' + newLine;
                } else {
                    problemFile += problem.question.trim() + newLine;
                }

                problemFile += '*/' + newLine + newLine;
                problemFile += 'using System;' + newLine + newLine;
                problemFile += 'namespace InterviewPreperationGuide.Core.LeetCode.Solution' + problem.number + newLine;
                problemFile += '{' + newLine;

                if (problem.code == null || problem.code.length == 0) {
                    problemFile += '// {{ MISSING CODE }}' + newLine;
                } else {
                    problem.code = problem.code.trim();
                    problem.code = problem.code.startsWith('undefined') ? problem.code.substring(9, problem.code.length - 1) : problem.code;
                    problem.code = problem.code.replace('public class Solution {', 'public class Solution { public void Init() { }' + newLine + newLine);

                    const startBraceCount = problem.code.replace(/[^{]/g, '').length;
                    const endBraceCount = problem.code.replace(/[^}]/g, '').length;

                    if (startBraceCount > endBraceCount) {
                        problem.code += '}';
                    }

                    problemFile += problem.code + newLine;
                }

                problemFile += '}' + newLine;

                const problemFilePath = config.codeFolderPath + problems[i].number + '.cs';
                config.writeFile(problemFilePath, problemFile);
            }
        }

        console.log('Code files craete for problems from json');
    };

    if (require.main === module) {
        init();
    }

    module.exports = app;
}());