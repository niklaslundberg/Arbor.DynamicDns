#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

ENV dynamic-dns:BaseUrl=
ENV dynamic-dns:Username=
ENV dynamic-dns:Password=
ENV dynamic-dns:Hostname=

COPY App/ ./
ENTRYPOINT ["dotnet", "Arbor.DynamicDns.dll"]