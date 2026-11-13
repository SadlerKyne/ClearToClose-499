# ClearToClose-499
Clear To Close: a .NET Blazor web app that tracks income, bills and debts and shows your debt-to-income ratio. CSE 499 senior project.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Run the app

```bash
git clone https://github.com/SadlerKyne/ClearToClose-499.git
cd ClearToClose-499/ClearToClose
dotnet run
```

Open the URL shown in the terminal (for example `http://localhost:5000`).

The SQLite database (`ClearToClose/Data/cleartoclose.db`) is created on first run and is not committed, so each of us has our own local data.

## Working on a task

Every sprint task gets its own small branch off `main`.

1. `git switch main` and `git pull`
2. `git switch -c <area>/<task>`, for example `income/income-page`
3. Commit with a short message such as `feat: add income page` or `fix: income total rounding`
4. Push the branch and open a pull request into `main`
5. Merge it, then delete the branch

## Favorite Quotes

> "PowerPoint makes us stupid." — Gen. James Mattis (Shawn Kyne).
> 
> "The future belongs to those who believe in the beauty of their dreams." - Eleanor Roosevelt (Monica Araceli Kyne)

