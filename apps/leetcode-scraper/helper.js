(function() {
    'use strict';

    const helper = {};
    const fs = require('fs');
    const path = require('path');
    const config = require('./config');

    helper.deleteContentsOfFolder = async(directory) => {
        if (!fs.existsSync(directory)) {
            fs.mkdirSync(directory, { recursive: true });
            return;
        }

        fs.readdir(directory, (err, files) => {
            if (err) throw err;

            for (const file of files) {
                fs.unlink(path.join(directory, file), (err) => {
                    if (err) throw err;
                });
            }
        });
    };

    helper.readFile = async(filePath) => {
        return new Promise((resolve, reject) => {
            fs.readFile(filePath, 'utf8', function(err, data) {
                if (err) {
                    reject(err);
                } else {
                    resolve(data);
                }
            });
        });
    };

    config.writeFile = function(fileName, data) {
        fs.exists(fileName, function(exists) {
            if (exists) {
                fs.appendFile(fileName, data, function(err) {
                    if (err) throw err;
                });
            } else {
                fs.writeFile(fileName, data, (err) => {
                    if (err) throw err;
                });
            }
        });
    };

    helper.deleteFile = async(fileName) => {
        fs.unlink(fileName, (err) => {
            if (err) throw err;
        });
    };

    config.isFilePresent = async(fileName) => {
        return new Promise((resolve, reject) => {
            fs.exists(fileName, function(exists) {
                if (exists) {
                    resolve(true);
                } else {
                    resolve(false);
                }
            });
        });
    };

    module.exports = helper;
}());