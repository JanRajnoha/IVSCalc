$MSBuild = ""

if([Environment]::Is64BitOperatingSystem)
{ $MSBuild = "C:\Program Files (x86)" }
else
{ $MSBuild = "C:\Program Files" }

return $MSBuild