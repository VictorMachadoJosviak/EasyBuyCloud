#EasyBuy Docs

urls para retorno json

#User Authentication

retornar todos os usuarios cadastrados GET http://localhost:53302/api/public/usuarios

buscar usuario por nome ex: "jureg" GET http://localhost:53302/api/public/usuario/jureg

cadastrar um usuario POST http://localhost:53302/api/public/cadastro

autentica usuario login POST http://localhost:53302/api/public/login

#Produtos

Retornar todos os produtos incluindo categoria e estabelecimento GET http://localhost:53302/api/public/produtos

retornar apenas produtos GET http://localhost:53302/api/public/apenasprodutos

buscar produtos por categoria ex "doces" GET http://localhost:53302/api/public/categoriaproduto/doces

buscar produtos por marca ex "salamitos" GET http://localhost:53302/api/public/produtos/salamitos

#MERCADOS

retorna todos os mercados GET http://localhost:53302/api/public/mercados

buscar mercado por nome ex "condor" GET http://localhost:53302/api/public/estabelecimentos/condor

buscar produto disponivel "true" or "false" GET http://localhost:53302/api/public/produtodisponivel/false

#CAETGORIAS

retorina todas as categorias GET http://localhost:53302/api/public/categorias

buscar categoria por tipo ex: "carnes" GET http://localhost:53302/api/public/categorias/carne
