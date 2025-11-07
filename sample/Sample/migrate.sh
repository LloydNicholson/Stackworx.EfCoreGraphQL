#!/usr/bin/env sh
set -e

rm -rf Migrations
dotnet ef migrations add Initial