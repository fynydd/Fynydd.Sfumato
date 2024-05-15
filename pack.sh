rm -r Fynydd.Sfumato/nupkg
source clean.sh
cd Fynydd.Sfumato
dotnet pack --configuration Release
cd ..
