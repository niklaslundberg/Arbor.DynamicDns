@ECHO OFF
IF EXIST DockerTemp (
  RMDIR DockerTemp /s /q
)

IF NOT EXIST DockerTemp (
  MKDIR DockerTemp
  MKDIR DockerTemp\App
)

XCOPY Arbor.DynamicDns\bin\Release\netcoreapp3.0\publish DockerTemp\App\ /e
COPY Dockerfile DockerTemp\Dockerfile

CD DockerTemp

docker build -t arbor-dynamicdns .

CD %~dp0