@echo off
echo Deleting all 'bin' and 'obj' directories...

REM Xóa tất cả thư mục bin
for /d /r %%d in (bin) do (
    if exist "%%d" (
        echo Deleting "%%d"
        rd /s /q "%%d"
    )
)

REM Xóa tất cả thư mục obj
for /d /r %%d in (obj) do (
    if exist "%%d" (
        echo Deleting "%%d"
        rd /s /q "%%d"
    )
)

echo Done.
pause
