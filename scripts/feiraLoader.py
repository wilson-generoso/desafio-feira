import json
import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
from feira import Feira

class FeiraLoader:
    __filename = ''
    feiras = []
    isLoaded = False

    def __init__(self, filename):
        self.__filename = filename

    def load(self):
        try:
            f = open(self.__filename)
            counter = 0

            for line in f:
                if counter > 0:
                    feira = Feira(line)
                    self.feiras.append(feira)
                counter += 1
            
            self.isLoaded = True
        except BaseException as err:
            print(err);
            self.isLoaded = False;
    
    def sendAll(self, baseUrl):

        if not self.isLoaded:
            self.load()

        if self.isLoaded and len(self.feiras) > 0:
            requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

            headers = {"Accept-Encoding":"*","accept":"text/plain","Content-Type":"application/json"}

            for feira in self.feiras:
                if not self.__send(baseUrl + '/api/v1/feira', feira, headers):
                    break
        else:
            print("As feiras não foram carregadas")

    def __send(self, url, feira, headers):
        try:
            response = requests.post(url, data = feira.toJson(), headers=headers, verify=False)
        except requests.exceptions.ConnectionError as connError:
            print("Não foi possível conectar com a API: {0}".format(connError))
        except BaseException as err:
            print(err)

        result = response.json()

        if response.status_code == 200:
            print("Feira {0} foi registrada com sucesso com identificador {1}".format(feira.nome, result["identificador"]))
            return True
        else:
            print("Não foi possível registrar a feira {0} => {1} (Feira: {2})".format(feira.nome, result, feira.toJson()))
            return False
