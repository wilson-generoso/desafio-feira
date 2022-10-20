import json

class Feira:
    longitude = ''
    latitude = ''
    setorCensitario = ''
    areaPonderacao = ''
    codigoDistritoIBGE = ''
    distritoMunicipal = ''
    codigoSubprefeitura = ''
    subprefeitura = ''
    regiaoMunicipio5Areas = ''
    regiaoMunicipio8Areas = ''
    nome = ''
    registro = ''
    logradouro = ''
    numeroLogradouro = ''
    bairro = ''
    pontoReferencia = ''    

    def __init__(self, strData):
        splittedData = strData.split(',')

        if len(splittedData) == 17:
            self.longitude = splittedData[1]
            self.latitude = splittedData[2]
            self.setorCensitario = splittedData[3]
            self.areaPonderacao = splittedData[4]
            self.codigoDistritoIBGE = splittedData[5]
            self.distritoMunicipal = splittedData[6]
            self.codigoSubprefeitura = splittedData[7]
            self.subprefeitura = splittedData[8]
            self.regiaoMunicipio5Areas = splittedData[9]
            self.regiaoMunicipio8Areas = splittedData[10]
            self.nome = splittedData[11]
            self.registro = splittedData[12]
            self.logradouro = splittedData[13]
            self.numeroLogradouro = self.__getNumber(splittedData[14])
            self.bairro = splittedData[15]
            self.pontoReferencia = splittedData[16].replace('\n','')[0:24]
        else:
            print('Tamanho de linha inválida: ', strData)

    def toJson(self):
        jsonData = json.dumps(self.__dict__)
        return jsonData

    def __getNumber(self, data):
        try:
            num = float(data)
            return str(int(num))
        except ValueError as err:
            print(err)
            return data
        
