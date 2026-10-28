---
name: git-commit
description: >-
  Use this skill whenever code, documentation, or configuration modifications have been made,
  or when the user asks for a git commit message, review of staged changes, or guidance on committing.
---

# Git Commit Skill

This skill ensures that whenever changes are made to the codebase or documentation, high-quality, standardized Conventional Commit messages are generated and provided.

## Workflow

1. **Inspect Changes**:
   - Check status: `git status -s`
   - Check modified files and line deltas: `git diff --stat`
   - If needed, inspect specific diffs: `git diff` or `git diff --cached`

2. **Determine Commit Type & Scope**:
   - `feat`: New feature, algorithm, application, or system design architecture
   - `fix`: Bug fix, dead link fix, calculation correction, typo repair
   - `docs`: Documentation only changes (e.g. README, interview notes, cheatsheets)
   - `refactor`: Code restructuring without changing behavior
   - `test`: Adding or updating test cases
   - `chore`: Build scripts, dependencies, markdownlint, CI configs
   - **Scope**: Identify the affected module (e.g., `(system-design)`, `(core)`, `(practice)`, `(chess)`, `(docs)`)

3. **Format Commit Message Output**:
   Always provide two clear options for the user:
   - **Option 1: One-Liner (Conventional Commit)**: A concise, imperative sentence (under 72 chars).
   - **Option 2: Detailed Multi-Line Commit**: A descriptive header with a bulleted breakdown of changes, rationale, and validations performed.

4. **Example Output Format**:

   ### Option 1: One-Liner

   ```bash
   git commit -am "<type>(<scope>): <concise summary in imperative mood>"
   ```

   ### Option 2: Detailed Multi-Line Commit

   ```bash
   git commit -am "<type>(<scope>): <concise summary in imperative mood>

   - Detailed change 1
   - Detailed change 2
   - Verification / tests performed"
   ```
