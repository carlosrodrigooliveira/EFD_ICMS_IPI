# EFD_ICMS_IPI

Biblioteca em C# .Net para manipular as informações contidadas no Escrituração Fiscal Digital (EFD ICMS IPI)  do arquivo TXT enviado ao fisco.

O governo adotou o padrão que cada linha é um registro sendo dividido as colunas por "|" (Pipe, código 124 da Tabela ASCII) e organizado hierarquicamente por níveis e regras definida no Guia Prático EFD, sendo a primeira coluna indica qual registro está sendo informado. 

EX.: "0150" - REGISTRO 0150: TABELA DE CADASTRO DO PARTICIPANTE

Nesta biblioteca iremos extrair no arquivo da EFD ICMS IPI as seguintes informações:

- Contribuinte
     
    - Registro "0000": ABERTURA DO ARQUIVO DIGITAL E IDENTIFICAÇÃO DA ENTIDADE
            
        - Este registro possui informações referente ao contribuinte da escrituração e o período da mesma.    
    
    - Registro "0005": DADOS COMPLEMENTARES DA ENTIDADE
          
         - Este registro possui informações referente ao endereço do contribuinte o nome fantasia.
     
- Participantes 

    - Registro "0150": TABELA DE CADASTRO DO PARTICIPANTE
        
        - Registro utilizado para informações cadastrais das pessoas físicas ou jurídicas envolvidas nas transações comerciais com o estabelecimento.

- Produtos
      
     - Registo: "0200": TABELA DE IDENTIFICAÇÃO DO ITEM (PRODUTO E SERVIÇOS)

        - Este registro possui informações das características referente aos produtos e serviços.
   
     - Registo: "0205": ALTERAÇÃO DO ITEM
        
        - Este registro é informado quando existe alteração de informações das características referente ao produto.
            
     - Registo: "0206": CÓDIGO DE PRODUTO CONFORME TABELA PUBLICADA PELA ANP

        - Este registro é informado quando o produto for controlado pela Agência Nacional de Petróleo (ANP) e tem por objetivo informar o código correspondente ao produto constante na tabela da mesma.
        
     - Registo: "0210": CONSUMO ESPECÍFICO PADRONIZADO
     
        - Este registro é informado caso exista produção e/ou consumo por outros produtos que utilizam o mesmo como insumo na produção.
                  
     - Registo: "0220": FATORES DE CONVERSÃO DE UNIDADES
      
        - Este registro tem por objetivo informar os fatores de conversão dos produtos / serviços informado no registro 0200

 
====================================================================================

Instituído pelo Decreto nº 6.022, de 22 de janeiro de 2007, o Sistema Público de Escrituração Digital (Sped) constitui-se em mais um avanço na informatização da relação entre o fisco e os contribuintes.

A Escrituração Fiscal Digital - EFD é um arquivo digital, que se constitui de um conjunto de escriturações de documentos fiscais e de outras informações de interesse dos Fiscos das unidades federadas e da Secretaria da Receita Federal do Brasil, bem como de registros de apuração de impostos referentes às operações e prestações praticadas pelo contribuinte.
Este arquivo deverá ser assinado digitalmente e transmitido, via Internet, ao ambiente Sped. 

Principais bloco:

- Bloco 0: Abertura, Identificação e Referências;
- Bloco B: Escrituração e Apuração do ISS;
- Bloco C: Documentos Fiscais I – Mercadorias (ICMS/IPI);
- Bloco D: Documentos Fiscais II – Serviços (ICMS)
- Bloco E: Apuração do ICMS e do IPI;
- Bloco G: Controle do Crédito de ICMS do Ativo Permanente – CIAP;
- Bloco H: Inventário Físico;

Os manuais e todas as informações referente ao projeto EFD ICMS IPI pode ser acessado no site:

http://sped.rfb.gov.br/
