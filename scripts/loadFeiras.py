from feiraLoader import FeiraLoader

# Inicializa carregador de feiras
loader = FeiraLoader('../doc/DEINFO_AB_FEIRASLIVRES_2014.csv')

# Envia todas as feiras encontradas para api de feiras
loader.sendAll('https://localhost:8001')
