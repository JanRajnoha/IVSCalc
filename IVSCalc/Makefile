ifeq ($(PROCESSOR_ARCHITEW6432),AMD64)
        Doxy= C:\Program Files\doxygen\bin\doxygen.exe
		MSBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
    else
        ifeq ($(PROCESSOR_ARCHITECTURE),AMD64)
            Doxy = C:\Program Files\doxygen\bin\doxygen.exe
			MSBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
        endif
        ifeq ($(PROCESSOR_ARCHITECTURE),x86)
            Doxy = C:\Program Files\doxygen\bin\doxygen.exe
			MSBuild="C:\Program Files\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe"
        endif
    endif
	
all:
	$(MSBuild) .\IVSCalc\IVSCalc.csproj /p:OutDir=..\Package;Configuration=Release;AppxBundle=Always;AppxBundlePlatforms="x86|x64|ARM"
pack:
	powershell Compress-Archive -Path ..\ -DestinationPath ..\xrajno09_xjuric27_xsalon00_xzubri00.zip
run:
	powershell start ms-windows-store://pdp/?productid=9ntzb53ggkn7
doc:
	$(Doxy) Doxyfile.dox