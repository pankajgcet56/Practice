#!/usr/bin/env bash


# IFS --> Internal Filed Seperator


COUNT=1

while IFS='' read -r LINE
do
    echo "LINE $COUNT: $LINE"
    ((COUNT++))
done < "$1"

exit 0