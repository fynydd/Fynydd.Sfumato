cd Fynydd.Sfumato
rm -r bin
rm -r obj
dotnet restore
cd ../

cd Fynydd.Sfumato.Tests
rm -r bin
rm -r obj
dotnet restore
cd ../
