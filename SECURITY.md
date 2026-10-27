# Security Policy

## Scope & Supported Versions

This repository is an open-source educational resource and reference monorepo for technical interview preparation covering Data Structures & Algorithms, System Design, and Engineering Management.

Because this project is continuously maintained rather than distributed as versioned binary packages, security updates and bug fixes are applied directly to the active `main` branch.

| Branch / Target | Supported          | Notes                                     |
| --------------- | ------------------ | ----------------------------------------- |
| `main`          | :white_check_mark: | Actively maintained with the latest fixes |
| Historical tags | :x:                | Older tags/commits are not patched        |

---

## Reporting a Vulnerability

We take the security of this project and its contributors seriously. If you discover a security vulnerability or potential threat in this repository (including sample applications, helper scripts, dependencies, or workflows), please report it responsibly.

### How to Report

- **Preferred Method:** Use [GitHub Private Vulnerability Reporting](https://github.com/tarunbatta/tech-interview-prep/security/advisories/new) to submit an advisory securely.
- **Alternative:** Contact the repository maintainer directly via GitHub profile: [@tarunbatta](https://github.com/tarunbatta).

> **Important:** Please do **not** report security vulnerabilities or leaked secrets via public GitHub issues, pull requests, or discussions.

### What to Include in Your Report

To help us triage and resolve the issue quickly, please provide:

1. **Description:** A concise explanation of the vulnerability and its potential impact.
2. **Affected Components:** The affected file(s), script(s), application, or workflow.
3. **Reproduction Steps:** Clear, step-by-step instructions or a proof-of-concept (PoC) demonstrating the issue.
4. **Proposed Fix (Optional):** Any recommended mitigations or code patches.

---

## Response Process & Timeline

When a vulnerability report is submitted:

1. **Acknowledgment:** We will acknowledge receipt of your report within 48 to 72 hours.
2. **Assessment:** We will investigate and validate the severity and impact of the reported vulnerability.
3. **Resolution:** A fix will be developed and merged into `main`. We will coordinate public disclosure or advisory publication as appropriate.
4. **Credit:** If you wish, we will gladly credit your responsible disclosure in the security advisory and release notes.

---

## Security Guidelines for Contributors

- **No Secrets or Credentials:** Never commit API keys, personal access tokens (PATs), passwords, private keys, or environment secrets to the repository.
- **Confidentiality:** Do not commit proprietary, confidential, or NDA-protected interview questions or company internal material.
- **Dependency Hygiene:** Sample applications (.NET, Node.js) are monitored via Dependabot for known dependency CVEs. Keep dependencies updated when making pull requests.
- **Safe Execution:** Always review shell scripts (`scripts/`) and dependencies prior to executing them in production or sensitive local environments.
