from keras.models import Sequential
from keras.layers import Dense, Activation, Dropout
from sklearn.model_selection import StratifiedKFold
import pandas as pd
import numpy as np

def load_data():
    df_read = pd.read_csv('block-design-clean.csv')
    df = df_read.where((pd.notnull(df_read)), 0)
    out_data = df['BLOCKRAW']
    in_data = df.drop('BLOCKRAW', 1)
    return in_data, out_data
 
def create_model(in_data, out_data, num_hidden_layers, num_hidden_nodes):
    outputs = [num_hidden_nodes for x in range(num_hidden_layers)]
    activate = ['relu' for x in range(num_hidden_layers)]
    drops = [0.05 for x in range(num_hidden_layers)]

    model = Sequential()
    model.add(Dense(output_dim=num_hidden_nodes, input_dim=len(in_data.columns)))
    model.add(Activation('relu'))
    
    for i in range(num_hidden_layers):
        model.add(Dense(output_dim=outputs[i]))
        model.add(Activation(activate[i]))
        # model.add(Dropout(drops[i]))
 
    model.add(Dense(output_dim=len(out_data.to_frame().columns)))
    model.add(Activation('relu'))
    model.compile(loss='mean_squared_error', optimizer='rmsprop', metrics=['accuracy'])
    return model

def train_model(model, in_data, out_data):
    hist = model.fit(in_data.as_matrix(), out_data.as_matrix(), nb_epoch=10)
    print(hist.history)
    return model

def test_model(model, in_data, out_data):
    score = model.evaluate(in_data.as_matrix(), out_data.as_matrix())
    return score

if __name__ == "__main__":
    n_folds = 10
    data, labels = load_data()
    skf = StratifiedKFold(n_splits=n_folds, random_state=None, shuffle=True)
    score = []
    count = 1
    for (train, test) in skf.split(data, labels):
        print("\nRunning fold " + str(count) +  "/" + str(n_folds))
        model = None
        model = create_model(data, labels, 5, 200)
        model = train_model(model, data.iloc[train], labels.iloc[train])
        score.append(test_model(model, data.iloc[test], labels.iloc[test]))
	count += 1
    acc = [x[1] for x in score]
    print(np.mean(acc))
