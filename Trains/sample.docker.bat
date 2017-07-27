@REM CREATE AND RUN THE DOCKER IMAGE SAMPLE
docker build . -f sample.dockerfile -t sample.trains
docker run sample.trains