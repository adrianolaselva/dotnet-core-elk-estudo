
# Solução desenvolvida

## Descrição

A solução proposta conta com uma apicação multiplataforma conforme proposto, para armazenamento faz uso do elasticsearch por ser uma das ferramentas mais adequada para este cenários, adicionei também um kibana para facilitar a visualização, pois ele conta com vários recursos para gerar radiadores de informações de acordo com a necessidade do cliente.

Na api de inserção de dados estou utilizando o SQS para enfileirar as os eventos e implementei um Worker para persistir os dados de forma assíncrona, a priori esta sendo indexado um evento por requisição, mais o ideal seria implementar uma inteligência para fazer bulk inserts (neste cenário eu utilizaria um logstash com um pipeline SQS => Elasticsearch) diminuindo o número de requisições ao elasticsearch.

Adicionei uma biblioteca para gerar a documentação com o Swagger, provendo um sandbox com as apis implementadas.

Como uma das premissas do teste é criar uma aplicação fácil de ser contruída e capaz de atender 10000 eventos por segundo, estou utilizando Docker para contruir o projeto e deixei um arquivo "docker-compose.prod.yml" com toda a stack da solução, adicicionei o Elasticsearch e o kibana, porém eu utilizaria um SaaS para não ter de me preocupar com a infraestrutura do cluster, assim deixando apenas a aplicação, a aplicação por sua vez estando em container possibilita o uso de um orquestrador, tal como swarm ou kubernetes.

## Documentação

[Documentação swagger](http://localhost:5000/swagger/index.html)

## Desenho da solução

![Arquitetura da solução](img/arquitetura-solucao.png)


## Rodar aplicação


### Pré requisitos

Para rodar a aplicação as seguintes variáveis de ambiente deverão ser preenchidas no arquivo ".env" na raiz da solução seguindo como modelo o arquivo ".env.dist":

```.env
AWS_ACCESS_KEY= #access key para acessar a AWS
AWS_SECRET_KEY= #secret key para acessar a AWS
AWS_SQS_QUEUE_URL_EVENT_RECEIVER= #URL do SQS para acessar serviço de fila na região us-east-1
ELASTICSEARCH_URL= #URL do elasticsearch
ELASTICSEARCH_INDEX= #Prefixo do índice no elasticsearch (obs: manter o nome "iot-receive")
```

Ex:

No projeto o arquivo "./src/analyst-challenge/appsettings.dev.json"

```json
{
  "AWS_ACCESS_KEY": "",
  "AWS_SECRET_KEY": "",
  "AWS_SQS_QUEUE_URL_EVENT_RECEIVER": "",
  "ELASTICSEARCH_URL": "http://elasticsearch:9200",
  "ELASTICSEARCH_INDEX": "iot-receive",
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

Nos testes arquivo "./tests/analyst-challenge.tests/appsettings.dev.json"

```json
{
  "AWS_ACCESS_KEY": "",
  "AWS_SECRET_KEY": "",
  "AWS_SQS_QUEUE_URL_EVENT_RECEIVER": "",
  "ELASTICSEARCH_URL": "http://localhost:9200",
  "ELASTICSEARCH_INDEX": "iot-receive",
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

Obs: Acabei deixando fixo a região da AWS sendo a "us-east-1", sendo a ssim o serviço do SQS deverá ser criado nela.

### Construir container

```sh
sudo docker build ./src/analyst-challenge/ --tag=adrianolaselva/analyst-challenge:0.0.1
```

### Rodar projeto

```sh
sudo docker run -it -p 5005:80 adrianolaselva/analyst-challenge:0.0.1 \
-e AWS_ACCESS_KEY= \
-e AWS_SECRET_KEY= \
-e AWS_SQS_QUEUE_URL_EVENT_RECEIVER=https://sqs.us-east-1.amazonaws.com/123123213121/queue-event-receiver \
-e ELASTICSEARCH_URL=http://localhost:9200/ \
-e ELASTICSEARCH_INDEX=iot-receive
```

#### Requisição responsável por enviar os eventos

```sh
curl -X POST \
  http://localhost:5000/v1/event-receivers \
  -H 'Content-Type: application/json' \
  -d '{
    "timestamp": 1539112021,
    "tag": "brasil.sudeste.sensor01",
    "valor": "912391232193912"
}'
```

### Rodar Stack completa para testes

```sh
sudo docker-compose -f docker-compose.prod.yml up --build
```

### ShellScript para subir solução

```sh
sudo docker-compose -f docker-compose.prod.yml up --build
```