# Mega School 1 App

[![](../../workflows/gh-pages/badge.svg)](../../actions)


Visit site @ https://megaschool1.github.io/mwr

# How to Fork
1. Fork this repository
2. Go to the repository you just created, then go to: `Settings` > `GitHub Pages` > set the source branch to `gh-pages-from-actions`
3. [Set permissions for GitHub Actions](https://stackoverflow.com/questions/73687176/permission-denied-to-github-actionsbot-the-requested-url-returned-error-403)
4. View your site at https://`your_user_name | your_organization_name`.github.io/`name_you_used_in_step1`
5. (optional) Enable Dependabot
    1. `Insights` > `Dependency graph` > `Dependabot` > Enable Dependabot 
    2. Create a branch named `dev` (branched from the `main` branch)

# Development
1. Run Stellar Horizon
```shell
docker run --rm -it -p 8000:8000 --name horizon stellar/quickstart --local
```
2. Run Stellar Horizon proxy
```shell
docker run --rm -it -p 8080:80 -p 8081:443 -e PROXIED_URL=http://host.docker.internal:8000 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORTS=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="password1" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v $env:USERPROFILE\.aspnet\https:/https/ horizonproxy
```

# References
* [Digital Bearer Cerficates](https://nakamotoinstitute.org/library/contracts-with-bearer/)
* [NSec WebAssembly Support Ticket](https://github.com/ektrah/nsec/issues/64)
* [zkVM](https://www.lita.foundation/blog/zero-knowledge-paradigm-zkvm)
