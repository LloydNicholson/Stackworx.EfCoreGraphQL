#!/usr/bin/env sh
set -e

if [ -z "${NUGET_API_KEY:-}" ]; then
  echo "❌ Error: NUGET_API_KEY is not set."
  exit 1
fi

for pkg in ./artifacts/*.nupkg; do
  dotnet nuget push "$pkg" \
    --source "https://api.nuget.org/v3/index.json" \
    --api-key "$NUGET_API_KEY" \
    --skip-duplicate
done