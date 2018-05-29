from keras.models import Sequential
from keras.layers import Dense, Activation

import pandas as pd
from sklearn.model_selection import train_test_split

data = pd.read_csv('train.csv', error_bad_lines =False)
y = data['label']
x = data.drop('label', 1)

x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.3, random_state=0)

model = Sequential()
model.add(Dense(output_dim=20,input_dim=len(x.columns)))
model.add(Activation('relu'))
model.add(Dense(output_dim=20))
model.add(Activation('relu'))
model.add(Dense(output_dim=1))
model.add(Activation('relu'))
model.compile(loss='mean_squared_error',optimizer='sgd',metrics=['accuracy'])

model.fit(x_train.as_matrix(), y_train.as_matrix(), nb_epoch=5)
loss_and_metrics = model.evaluate(x_test.as_matrix(), y_test.as_matrix())
print(loss_and_metrics)
