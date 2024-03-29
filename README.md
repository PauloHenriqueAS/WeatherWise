# ☁️ WeatherWise

<p align="center">
<img src="https://img.shields.io/badge/STATUS-EM%20DESENVOLVIMENTO-brightgreen"/>
</p>

## ⚙️ Tecnologias Utilizadas

<div align="center">
    <div style="display: inline_block"><br>
        <img align="center" alt="HTML5" height="30" width="40" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-original.svg">
        <img align="center" alt="CSS3" height="30" width="40"  src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-original.svg">
        <img align="center" alt="Bootstrap" height="30" width="40"  src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/bootstrap/bootstrap-original.svg">
        <img align="center" alt="DotNet Core" height="30" width="40" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg">
        <img align="center" alt="Javascript" height="30" width="40"src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/javascript/javascript-original.svg">
        <img align="center" alt="Amazon Web Services" height="30" width="40" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/amazonwebservices/amazonwebservices-original.svg">
        <img align="center" alt="Postgresql" height="30" width="40" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original.svg">        
    </div>
</div>

<hr>

- **Models:**
    - AirPollution.cs
    - City.cs
    - Cloud.cs
    - Components.cs
    - Coordinate.cs
    - CurrentWeather.cs
    - Forecast.cs
    - Main.cs
    - Sys.cs
    - TypesUser.cs
    - User.cs
    - UserCredentials.cs
    - Weather.cs
    - Wind.cs
- **Controllers:**
    - AirPollutionController.cs
    - CoordinateController.cs
    - UserController.cs
    - WeatherController.cs
- **DAL:**
    - AirPollutionDAL.cs
    - UserDAL.cs
    - WeatherDAL.cs
- **BLL:**
    - AirPollutionBLL.cs
    - CoordinateBLL.cs
    - UserBLL.cs
    - WeatherBLL.cs
- **API:**
    - Api.cs
    - OpenWeatherApi.cs
    - NominatimApi.cs
- **Extensions:**
    - ConnectionStrings.cs
- **Helpers:**
    - Common.cs
    - DataBase.cs
    - ResponseApi.cs

<div align="center">
    <h1> :octocat: GIT  </h1> 
</div>

O GIT é uma ferramenta que te ajuda a controlar melhor as versões do seu código, ou seja, é uma forma de melhorar a sua organização com códigos.


**Após ver o que é GIT, instale-o em sua máquina**

```
    Git Download:  https://github.com/git-guides/install-git
```

### Como eu faço para utilizar o GIT nesse projeto (aqui começa a entender um pouco de GITHUB)

- Primeiramente acesse o site do GITHUB (é necessário criar uma conta), após isso entre no link: https://github.com/PauloHenriqueAS/WeatherWise

- Nessa página você irá ver um monte de abas:
    - Code: é onde está o que queremos, os arquivos do nosso site, nosso código.
    - Issues: quando você quer fazer um comentário, achou algum problema no código de alguém ou no seu mesmo, você abre uma **issue** para isso e quando resolver o problema, você a fecha.
    - Pull request: toda vez que você fizer uma alteração no código e quer colocá-la em outra branch é ideal que você faça um pull request, da sua branch para a branch que você quer, mas antes certifique-se de que está tudo correto.

- Agora vamos colocar o código que está no Github em sua máquina?
    - Na aba code, você vai ver um botão verdinho mais para o lado direito da página, nele está escrito **`Code`**, clique nele e copie o link HTTPS que foi dado;
    - Com o GIT instalado, abra o terminal (linux) ou CMD (windows) e digite o seguinte comando:
        ```
        git clone https://github.com/PauloHenriqueAS/WeatherWise.git
        ```
        Pronto você possui todas o conteúdo atual do site em sua máquina
    - Pesquise sobre branches e gitflow - após isso volte aqui;
    - Agora que já entende de branches, vamos criar a sua:
        - dê o comando abaixo para ver as branches que estão sendo utilizadas: 
        ```
        git branch
        ```
        
        - Agora, para criar sua branch **igualzinha** a branch que você está no momento, dê o comando:
        ```
        git branch feature/seu-nome/o-que-vai-fazer-no-projeto
        ```
        Exemplo:
        ```
        git branch feature/Mateus/Conect-API
        ```
        
        - Se quiser copiar outra branch que não seja a que está, dê o comando:
            ```
            git checkout branch-que-voce-deseja-estar
            ```
        **Pronto, você já tem sua branch exclusiva**
    - Agora você precisa mudar da branch que está para a branch que criou, para fazer isso, use o mesmo comando acima:
    ```
    git checkout feature/seu-nome/o-que-vai-fazer-no-projeto
    ```
    Exemplo:
    ```
    git checkout feature/Mateus/Conect-API
    ```
    - Agora comece a fazer suas alterações;
    - Após alterar algo relevante, você precisa registrar isso, é o que chamamos de commits:
        - dê o comando abaixo para ver as alterações que você fez:
        ```
        git status
        ```
        - Agora dê o comando abaixo para o git monitorar todos os arquivos seus que foram editados:
        ```
        git add .
        ```
        - Após isso, dê novamente o `git status` só para você ver a mudança
        - Agora, podemos commitar:
            - dê o comando abaixo para registrar sua mudança no GIT:
            ```
            git commit -m "Aqui você coloca uma BREVE descrição do que acabou de alterar, por exemplo - criação podcast"
            ```
        - Dê novamente um `git status`, viu que agora não tem nada? É porque está salvo suas alterações, para ver isso, execute o comando:
        ```
        git log
        ```

        Para sair, aperte a tecla q

**Esse é o básico do GIT, você aprenderá mais coisas ao longo do tempo**

### Subindo o código para o GITHUB
- Após realizar as alterações dê o comando:
```
git push origin feature/seu-nome/o-que-vai-fazer-no-projeto
```

**Com o tempo você aprenderá estratégias melhores, o que é um FORK, como fazer um PULL REQUEST de forma correta, a fazer um MERGE ou REBASE, a entender melhor, para isso você precisa praticar, é capaz de fazer tudo, vou deixar um link abaixo para que você possa treinar**

```
Git visualizing: https://git-school.github.io/visualizing-git/
```

Entre no link e divirta-se!

<hr>

# 🚧 Separação dos Projetos
- ## <img height="30" width="30" src="https://img.icons8.com/color/48/null/code.png"/> Pasta API (Application Programming Interface)
  - Arquivos Api e solução Api
- ## 🖼️ Pasta UI (User Interface)
  - SRC -> Paginas HTML, Assets para a estilização da página e Códigos Javascript.
  - Arquivo Bootstrap Studio

- ## 🔃 Pasta Rotina
  - Arquivos Rotina e solução rotina
  
# ☁️ Weather Wise API
Principal função sendo a consulta de informações sobre o tempo atual, a previsão do tempo para os próximos 5 dias e a poluição do ar na API externa [OpenWeather](https://openweathermap.org/) API, informações de latitude e longitude pela [Nominatim](https://nominatim.org/) API, e por fim salvando as informações obtidas nas tabelas de nosso banco de dados [PostgreSQL](https://www.postgresql.org/) que se encontra hospedado online na [AWS (Amazon Web Services)](https://aws.amazon.com/pt/free/?trk=e4d1a24a-13d1-4019-b365-e284fded1202&sc_channel=ps&ef_id=Cj0KCQjw0tKiBhC6ARIsAAOXutnuX91tteylv0xn_QAylMx7Q0_rob3W8Afo_jZPeUWhqAWmiZy_TKAaAs-eEALw_wcB:G:s&s_kwcid=AL!4422!3!454435137069!e!!g!!aws!10758390150!106168762236&all-free-tier.sort-by=item.additionalFields.SortRank&all-free-tier.sort-order=asc&awsf.Free%20Tier%20Types=*all&awsf.Free%20Tier%20Categories=*all).

# 🔃 Rotina Weather Wise
Rotina com o objetivo de consulta a API externa [OpenWeather](https://openweathermap.org/) API e alimentar o banco de dados a cada 3 horas com informações sobre o tempo atual, poluição do ar e previsão do tempo a fim de sempre termos dados atualizados e para a criação de dashboards disponíveis no Front-End.

# <img height="30" width="30" src="https://img.icons8.com/ios/50/null/database--v1.png"/>  Modelagem do Banco de Dados 
![Imagem BancoDados](Material_Grafico/dbWeather.png)

# <img height="30" width="30" src="https://img.icons8.com/external-others-pike-picture/50/null/external-creative-web-design-development-others-pike-picture-2.png"/> Prints DashBoards
- ## DashBoard
![Imagem BancoDados](Material_Grafico/dashboard.jpeg)



<hr>

<div align="center">
    <h1> 🚀 Participantes do projeto </h1>

| [<img src="https://avatars.githubusercontent.com/u/65378419?v=4" width="100"><br><sub>@PauloHenriqueAS</sub>](https://github.com/PauloHenriqueAS)  |  [<img src="https://avatars.githubusercontent.com/u/43917038?v=4" width="100"><br><sub>@matheusjreis</sub>](https://github.com/matheusjreis) | [<img src="https://avatars.githubusercontent.com/u/60183242?v=4" width="100"><br><sub>@guilhermefsc</sub>](https://github.com/guilhermefsc)  | [<img src="https://avatars.githubusercontent.com/u/52581803?v=4" width="100"><br><sub>@ricarterick</sub>](https://github.com/ricarterick)  |
| ------------ | ------------ | ------------ | ------------ |
| [<img src="https://avatars.githubusercontent.com/u/76082388?v=4" width="100"><br><sub>@cerutti542</sub>](https://github.com/cerutti542)  | [<img src="https://avatars.githubusercontent.com/u/84159269?v=4" width="100"><br><sub>@carlos-000-carlos</sub>](https://github.com/carlos-000-carlos)  |  |  
</div>

<hr>

![Gif ClimaTempo](Material_Grafico/ClimaTempo.gif)
