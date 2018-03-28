#!/bin/bash
#courtesy of discord.js

#exit if any command fails
set -e

#Figure out the source of the build
if [ -n "$TRAVIS_TAG" ]; then
  echo -e "\e[36m\e[1mBuild triggered for tag \"${TRAVIS_TAG}\"."
else
  echo -e "\e[36m\e[1mBuild triggered for branch \"${TRAVIS_BRANCH}\"."
fi
#PR
if [ "$TRAVIS_PULL_REQUEST" != "false" ]; then
  echo -e "\e[36m\e[1mBuild triggered for PR #${TRAVIS_PULL_REQUEST} to branch\"${TRAVIS_BRANCH}\""
fi

dotnet restore
dotnet test MakiseSharp.Tests/MakiseSharp.Tests.csproj
dotnet publish MakiseSharp/MakiseSharp.csproj -c Release
