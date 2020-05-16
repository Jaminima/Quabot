while (true) do

echo Compiling..
dotnet publish -c release -r ubuntu.18.04-x64 --self-contained -o ./TestApp/bin/Publish
echo Compiled
echo Press enter to recompile.

read
done