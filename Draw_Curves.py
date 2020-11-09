# This is a sample Python script.

# Press ⌃R to execute it or replace it with your code.
# Press Double ⇧ to search everywhere for classes, files, tool windows, actions, and settings.

import matplotlib.pyplot as plt
import numpy as np

def Tracer():

    M = np.loadtxt('/Users/macintosh/Desktop/courbes.txt')
    M = [int(i)/1000000 for i in M]
    Spot = M[0 : len(M)//2]
    Forward = M[len(M)//2 : len(M)]
    indice = [i for i in range(len(Spot))]
    plt.title("Courbes de taux")
    plt.plot(indice, Spot, label = "Taux Spot")
    plt.plot(indice, Forward, label = 'Taux Forward')
    plt.legend()
    plt.show()  # affiche la figure a l'ecran


# Press the green button in the gutter to run the script.
if __name__ == '__main__':

    Tracer()

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
