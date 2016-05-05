#!/bin/sh

version="1.0.1"
nuspec="Xamarin.Forms.NavigationExtensions.nuspec"

mono nuget.exe pack $nuspec -Version $version