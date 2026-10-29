# LeetCode Scraper

Scrapes LeetCode problems using Puppeteer and Cheerio, caches problem metadata as JSON, and generates C# solution stub files (`.cs`) for local debugging and practice.

## Setup

```bash
npm --prefix apps/leetcode-scraper install
```

## Run

From the repository root:

```bash
npm run start:leetcode-scraper
```

Or directly inside `apps/leetcode-scraper`:

```bash
node index.js
```

## Configuration

See [`config.js`](config.js) to customize:

- `problemsFolderPath`: Directory for cached per-problem JSON files (`problems/json/`).
- `codeFolderPath`: Directory for generated C# stub files (`problems/cs/`).
- `metadataFilePath`: Path to cached problem list metadata (`problems/_metadata.json`).
- `selectedlanguage`: Target language selector on LeetCode (defaults to `lang-select-C#`).
- `isRefetchMetadata` / `isRefetchProblems` / `enableScraping`: Flags controlling whether to re-fetch from LeetCode or regenerate `.cs` stubs from cached JSON.

## Notes

- Generated `.cs` stubs in `problems/cs/` are ignored by git; curated and solved C# implementations live in [`../../src/leetcode/csharp/`](../../src/leetcode/csharp/).
