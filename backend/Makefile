appver=latest

#vars
IMAGENAME=service-release-manager
REPO=registry.k3s.devnico.cloud
IMAGEFULLNAME=${REPO}/${IMAGENAME}:${appver}

help:
	    @echo "Makefile arguments:"
	    @echo ""
	    @echo "appver: set the app version defaults to 'latest'"
	    @echo ""
	    @echo "Makefile commands:"
	    @echo "build"
	    @echo "buildm1"

.DEFAULT_GOAL := build

build:
	    @docker build -t ${IMAGEFULLNAME} .
		@docker push ${IMAGEFULLNAME}


buildm1:
	    @docker buildx build --platform linux/amd64 -t ${IMAGEFULLNAME} --push .
