$Doxy = ""

if([Environment]::Is64BitOperatingSystem)
{ $Doxy = "C:\Program Files\doxygen\bin\doxygen.exe" }
else
{ $Doxy = "C:\Program Files\doxygen\bin\doxygen.exe" }

return $Doxy