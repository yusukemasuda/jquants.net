# Branching Strategy

JQuants.Net adopts Git Flow as its branching strategy.

## Branch Structure

```text
main
│
├─ develop
└─ feature/*
```
Future branch types such as `release/*` and `hotfix/*` are intentionally omitted for now and will be introduced only when operational requirements justify the additional workflow complexity.

### Main

The `main` branch always contains production-ready code.

Official releases are created from this branch.

### Develop

The `develop` branch is the integration branch for ongoing development.

Completed features are merged into this branch.

### Feature

Feature branches are created from `develop`.

Naming convention:

```text
feature/<feature-name>
```

Examples:

```text
feature/gitversion
feature/github-actions
feature/daily-quotes
```

### Release and Hotfix Branches

Release and hotfix branches are currently not used.

JQuants.Net is currently maintained by a single developer and follows a lightweight workflow:

```text
feature/* → develop → main
```

At the current project size, dedicated release and hotfix branches are considered unnecessary.

These branch types may be introduced in the future if project complexity, release frequency, or maintenance requirements increase.

---

# Merge Strategy

JQuants.Net uses different merge strategies depending on the target branch.

## Feature → Develop

Feature branches are merged into `develop` using **Squash Merge**.

Purpose:

- Keep the develop history clean.
- Ensure a single Conventional Commit represents each feature.
- Simplify version determination through GitVersion.

Example:

```text
feature/daily-quotes

    feat: create request model
    fix: unit tests
    docs: update documentation

↓

develop

    feat: add daily quotes endpoint
```

## Develop → Main

The `develop` branch is merged into `main` using **Merge Commit**.

Purpose:

- Preserve release history.
- Clearly identify release points.
- Retain the relationship between production releases and development history.

Example:

```text
main

    Merge branch 'develop'
```

---

# Versioning

## Semantic Versioning

JQuants.Net follows [Semantic Versioning 2.0.0](https://semver.org/).

Version numbers use the following format:

```text
MAJOR.MINOR.PATCH[-PRERELEASE]
```

Examples:

```text
0.1.0
0.2.0
1.0.0

0.2.0-alpha.1
0.2.0-beta.1
```

### Pre-release Labels

Pre-release labels are automatically assigned according to the branch type.

| Branch | Label | Example |
|----------|----------|----------|
| feature/* | alpha | 0.2.0-alpha.1 |
| develop | beta | 0.2.0-beta.1 |
| main | none | 0.2.0 |

Pre-release versions are intended for development and validation purposes and should not be considered production releases.

---

## Conventional Commits

JQuants.Net follows the [Conventional Commits](https://www.conventionalcommits.org/) specification.

All commit messages should follow the Conventional Commits format.

Examples:

```text
fix: correct pagination handling

feat: add daily quotes endpoint

feat!: redesign equities client API
```

Conventional Commits provide a consistent commit history and serve as the basis for automatic version incrementing.

---

## Version Increment

Version numbers are automatically incremented using GitVersion.

The increment type is determined from the title of the Pull Request merged into `main`.

### Patch

Use:

```text
fix: description
```

Example:

```text
fix: correct pagination handling
```

Result:

```text
0.1.0 → 0.1.1
```

---

### Minor

Use:

```text
feat: description
```

Example:

```text
feat: add daily quotes endpoint
```

Result:

```text
0.1.0 → 0.2.0
```

---

### Major

Use either:

```text
feat!: description
```

or include:

```text
BREAKING CHANGE:
```

Example:

```text
feat!: redesign equities client API
```

or

```text
feat: redesign equities client API

BREAKING CHANGE: GetListedIssuesAsync now requires a request object.
```

Result:

```text
0.2.0 → 1.0.0
```

### Notes

Only Pull Requests merged into `main` participate in release version calculation.

Commit messages in feature branches and Pull Requests targeting `develop` do not directly affect released version numbers.
