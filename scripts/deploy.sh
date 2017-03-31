#!/bin/bash
cd MakiseSharp/bin/Release/netcoreapp1.1
rm -R publish/runtimes/win
rm publish/*.pdb

cd publish/ && tar czf ../package.tgz . && cd .. 

export SSHPASS=$DEPLOY_PASS
echo "Sending the package..."
sshpass -e scp -o stricthostkeychecking=no package.tgz $DEPLOY_USER@$DEPLOY_HOST:$DEPLOY_PATH
sleep 3
sshpass -e ssh $DEPLOY_USER@$DEPLOY_HOST $DEPLOY_PATH/deploy.sh
