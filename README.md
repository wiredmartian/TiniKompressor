# TiniKompressor

Simple C# Console utility app that tranverses the folder structure and locates image files in the root and subdirectories.
It then compresses all the images using the TinyPNG API.

# How To Run

1. Go to https://tinypng.com/developers to create an API 
2. Add the key in App.config on the line `<add key="TinyKey" value="API KEY"/>`
3. Run the app
4. Paste the path to the folder with files you wanna to compress, e.g `C:\Dev\Hac\Awesome Stuff`
