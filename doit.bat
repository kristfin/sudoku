@echo off

for %%f in (Hard Medium Easy) do (	
	dotnet run -- --size 3 --count 1 --file %tmp%\%%f.html --output Html --level %%f
	%tmp%\%%f.html
)

goto end

Info.Obak.Sudoku 1.0.0
Copyright (C) 2019 Info.Obak.Sudoku

  -o, --output    Outputmode for generated sudokus. Text or Html supported.

  -f, --file      Output file.  If none, stdout is used.

  -c, --count     How many sudokus to generate.

  --size          How big should the sudokos be.  Only 3, 4 and 5 is supported.

  --seed          Initial seed for the sudoku.

  -l, --level     (Default: Medium) How hard should the sudokus be.

  --help          Display this help screen.

  --version       Display version information.

 :end