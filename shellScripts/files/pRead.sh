#!/usr/bin/env bash

COUNT=1
while IFS='' read -r LINE
do
    echo "Lien #$COUNT = $LINE"
    if [ $COUNT -ge 3 ]
    then
        break
    fi
    ((COUNT++))
done < "$1"