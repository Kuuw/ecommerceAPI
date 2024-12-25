#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Development

commands=(
  "azurite"
  "cd ./PL/ && dotnet run --launch-profile \"https\""
)

PID_LIST=""

for cmd in "${commands[@]}"; do
  echo "Process \"$cmd\" started"
  bash -c "$cmd" & pid=$!
  PID_LIST+=" $pid"
done

trap "kill $PID_LIST" SIGINT

echo "Parallel processes have started"

wait $PID_LIST

echo
echo "All processes have completed"