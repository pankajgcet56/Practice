#!/usr/bin/env bash

echo "The Path is $PATH"

echo "The Editor is $EDITOR"

if [ -z $EDITOR ]
then
    echo "Editor is not set"
fi

# -------------- Basic Env variables---------------------------------------------------------------------------

# HOME - user's home directory
# PATH - directories which are searched for commands
# HOSTNAME - hostname of Machine 
# SHELL - shell that is being used 
# USER - user of session
# TERM - type of command line terminal which are being used 


echo "Home = $HOME"
echo "Path = $PATH"
echo "Hostname = $HOSTNAME"
echo "Shell = $SHELL"
echo "User = $USER"
echo "Home = $HOME"
echo "Term = $TERM"

exit 0
