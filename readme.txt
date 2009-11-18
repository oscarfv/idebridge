Installation from Sources:
 1: Download IdeBridge sources.
 2: Download SharpDevelop sources :
    - option 1:
       a: from the official site: http://www.icsharpcode.net/OpenSource/SD/ download and extract SharpDevelop_3.1.0.4977_Source.zip
       b: open a cmd.exe in IdeBridge/PatchSharpDevelop/
       c: launch patch.bat <path-to-your-sharpdevelop-sources>.
    - option 2 :
       * download the already patched SharpDevelop sources from IdeBridge google code page.
 3: Compile the patched SharpDevelop (using either <path-to-your-sharpdevelop-sources>/src/releasebuild.bat or <path-to-your-sharpdevelop-sources>/src/debugbuild.bat)
 4: update the reference path to the SharpDevelop dlls : in the file IdeBridge/IdeBridge.csproj.user update the ReferencePath value to <path-to-your-sharpdevelop-sources>/bin
 5: copy the directory <path-to-your-sharpdevelop-sources>\AddIns\AddIns\BackendBindings\CSharpBinding in IdeBridge/bin/Addins
 8: Compile the IdeBridge solution.
