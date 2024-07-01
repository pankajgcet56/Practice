#!/usr/bin/env bash

DELAY=$1

if [ -z $DELAY ]
then 
    echo "Put dealy"
fi

echo " Going to Sleep $DELAY Delay"

sleep $DELAY

echo "Wake Up"
exit 0