import pandas as pd
from sklearn import cross_validation
from keras.models import Sequential
from keras.layers import Dense, Activation, Dropout

from hyperopt import Trials, STATUS_OK, tpe
from hyperas import optim
from hyperas.distributions import choice, uniform, conditional

# Get hyperopt working...
# http://stackoverflow.com/questions/35978549/pip-install-hyperopt-and-hyperas-fail/35978878
# https://www.reddit.com/r/MachineLearning/comments/46myc4/hyperas_keras_hyperopt_a_very_simple_wrapper_for/?st=iu7gxt6q&sh=0e61f9e0

def data():
    data = pd.read_csv('block-design-clean.csv')
    y = data['BLOCKRAW'].as_matrix()
    x = data.drop('BLOCKRAW', 1).as_matrix()

    x_train, x_test, y_train, y_test = cross_validation.train_test_split(x, y, test_size=0.3)
    return x_train, y_train, x_test, y_test


def model(x_train, y_train, x_test, y_test):
    # 25 > 10 (relu) dropout > 10 (relu) dropout > 1 (relu)
    model = Sequential()
    model.add(Dense(output_dim={{choice([5, 10, 15, 20, 25, 30, 50, 75, 100, 200, 500])}}, input_dim=x_train.shape[1]))
    model.add(Activation({{choice(['relu','sigmoid','tanh','linear'])}}))
    model.add(Dropout({{uniform(0, 1)}}))
    # If we choose 'four', add an additional fourth layer
    if conditional({{choice(['extra-layer', 'no'])}}) == 'extra-layer':
        model.add(Dense(output_dim={{choice([5, 10, 15, 20, 25, 30, 50, 75, 100, 200, 500])}}))
        model.add(Activation({{choice(['relu','sigmoid','tanh','linear'])}}))
        model.add(Dropout({{uniform(0, 1)}}))
    model.add(Dense(output_dim=1))
    model.add(Activation({{choice(['relu','sigmoid','tanh','linear'])}}))

    model.compile(loss='mean_squared_error', optimizer={{choice(['rmsprop', 'adam', 'sgd'])}}, metrics=['accuracy'])
    model.fit(x_train, y_train,
              batch_size={{choice([32, 64, 128])}},
              nb_epoch=10,
              validation_data=(x_test, y_test))
    score, acc = model.evaluate(x_test, y_test)
    print('Test accuracy:', acc)
    return {'loss': -acc, 'status': STATUS_OK, 'model': model}


if __name__ == '__main__':
    best_run, best_model = optim.minimize(model=model,
                                          data=data,
                                          algo=tpe.suggest,
                                          max_evals=500,
                                          trials=Trials())
    X_train, Y_train, X_test, Y_test = data()
    print("Evalutation of best performing model:")
    print(best_model.evaluate(X_test, Y_test))
