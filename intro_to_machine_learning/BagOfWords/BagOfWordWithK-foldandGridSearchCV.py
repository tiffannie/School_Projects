import re
import nltk
import os
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import StratifiedKFold
from sklearn.grid_search import GridSearchCV
from mybeautifulloop import mybeautifulloop
from bs4 import BeautifulSoup
from nltk.corpus import stopwords
import pandas as pd
import numpy as np

def load_data(train,train_data_features):
    out_data = train.iloc[:,1]
    in_data = train_data_features
    print in_data.shape, out_data.shape
    return in_data, out_data


if __name__ == '__main__':
    train = pd.read_csv("subjective.tsv", header=0, \
			 delimiter="\t", quoting=3)
    test = pd.read_csv("wikitest.tsv", header=0, \
			 delimiter="\t", quoting=3)

    print 'The first review is:'
    print train["review"][0]

    raw_input("Press Enter to continue...")
  

    print 'download data if you did not have it'
    nltk.download() 
    
    clean_train_reviews = []

    num_reviews = train["review"].size

    print "Cleaning and parsing the test set movie reviews...\n"
    for i in xrange(0,num_reviews):
        clean_train_reviews.append(" ".join(mybeautifulloop.review_to_wordlist(train["review"][i], True)))

    print "the new clean_train_reviews"
    print clean_train_reviews
    
   
    print "Creating the bag of words...\n"


    vectorizer = CountVectorizer(analyzer = "word",   \
                             tokenizer = None,    \
                             preprocessor = None, \
                             stop_words = None,   \
                             max_features = 5000)

    train_data_features = vectorizer.fit_transform(clean_train_reviews)
    

    train_data_features = train_data_features.toarray()

    print "here is train_data_features" 
    print train_data_features
  
 
    print "here is vocab"
    vocab = vectorizer.get_feature_names()
    print vocab

    print "here is the count,tag"

    dist = np.sum(train_data_features, axis=0)
    for tag, count in zip(vocab,dist):
        print count, tag
      
    print "Training the random forest"

    # load dataas
    n_folds=2
    data, labels = load_data(train, train_data_features) #where to load the data in (x=data,y=labels)

    # skf sets up the splits

    skf = StratifiedKFold(n_splits=n_folds, random_state=None, shuffle=True)
    acc = [] # [] means empty list, we gonna keep track ot it!
    count = 1 # we will start at count
    model = RandomForestClassifier(n_jobs=-1,max_features='auto' , n_estimators = 100)
    param_grid = {
    'n_estimators': [100, 300],
    'max_features': ['auto','sqrt','log2']
}
    CV_model= GridSearchCV(estimator=model, param_grid=param_grid, cv=3)    

    for (train1, test1) in skf.split(data, labels):
        print("\nRunning fold " + str(count) +  "/" + str(n_folds))
        #model = None # delete the old models
        model = CV_model.fit(data[train1],labels[train1])
        current_score=model.score(data[test1], labels[test1])
        print current_score
        acc.append(current_score)
        count += 1

    print(np.mean(acc))


    clean_test_reviews = []

    print "Cleaning and parsing the test set movie reviews...\n"
    for i in xrange(0,len(test["review"])):
        clean_test_reviews.append(" ".join(mybeautifulloop.review_to_wordlist(test["review"][i], True)))

    test_data_features = vectorizer.transform(clean_test_reviews)
    test_data_features = test_data_features.toarray()

    print "Predicting test labels...\n"
    result = model.predict(test_data_features)

    output = pd.DataFrame( data={"id":test["id"], "sentiment":result} )

    output.to_csv('Bag_of_words_Positive.csv', index=False, quoting=3)
    print "Wrote results to Bag_of_Words_model_Emotion.csv"


