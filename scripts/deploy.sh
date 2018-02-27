#!/bin/bash
export SSHPASS=$DEPLOY_PASS
echo "Sending the package..."

docker save makisekurisu | bzip2 | pv | \
/usr/bin/sshpass -e ssh -o StrictHostKeyChecking=no $DEPLOY_USER@$DEPLOY_HOST 'bunzip2 | docker load' 

sleep 3
sshpass -e ssh $DEPLOY_USER@$DEPLOY_HOST $DEPLOY_PATH/deploy.sh
