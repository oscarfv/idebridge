Installation from Sources:
 1: Download IdeBridge sources.
 2: Download SharpDevelop sources :
    - option 1:
       * from the official site: http://www.icsharpcode.net/OpenSource/SD/ download SharpDevelop_3.1.0.4977_Source.zip
       * open a cmd.exe in IdeBridge/PatchSharpDevelop/
       * launch patch.bat <path-to-your-sharpdevelop-sources>.
    - option 2 :
       * download the already patched SharpDevelop sources from IdeBridge google code page.
 3: Compile the patched SharpDevelop (using either <path-to-your-sharpdevelop-sources>/src/releasebuild.bat or <path-to-your-sharpdevelop-sources>/src/debugbuild.bat)
 4: - option 1: Copy the dll, exe, and pdb files from <path-to-your-sharpdevelop-sources>/bin to IdeBridge/Lib
    - option 2: or update the reference path to the SharpDevelop dlls in IdeBridge Project.
 5: Choose your binary directory where to store the compiled binaries, and update the build output path of IdeBridge project and IdeBridgeCompiler project (both projects should have the same binary directory).
 6: copy the directory data and Addins located in IdeBridge/ToBeCopiedInYouBinDirectory to your binary directory.
 7: copy the directory <path-to-your-sharpdevelop-sources>\AddIns\AddIns\BackendBindings\CSharpBinding in <your-binary-directory>/Addins
 8: Compile the IdeBridge solution.
