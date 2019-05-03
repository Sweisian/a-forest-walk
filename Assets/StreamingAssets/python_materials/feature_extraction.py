import librosa
import math
import numpy as np
import os
import json
import codecs
import ntpath
from madmom.features.onsets import OnsetPeakPickingProcessor, RNNOnsetProcessor


def get_onsets(path):
    """
    Finds onsets of audio file
    :param path: Path to audio file
    :return: List of times (in seconds) corresponding to onsets
    """
    processor = OnsetPeakPickingProcessor(threshold=0.45, combine=.2)
    act = RNNOnsetProcessor()(path)
    onsets = processor(act)
    onsets = onsets.tolist()
    return onsets


def onsets_to_amplitude(onsets, path):
    """
    :param onsets: list of times where onsets occur
    :param path: path for audio file
    :return: list of signal amplitudes at times in onsets list
    """
    y, sr = librosa.load(path, sr=None)
    samples_in_time = librosa.core.samples_to_time(y, sr=sr)
    wanted_indexs = np.searchsorted(samples_in_time, onsets)
    amp_array = y[wanted_indexs]
    return amp_array


def onsets_to_amplitudes(onsets, path):
    """
    :param onsets: List of times (in seconds) corresponding to onsets
    :param path: location to audio file
    :return: List of amplitudes corresponding to times where onsets occur
    """
    y, sr = librosa.load(path, sr=None)
    duration = librosa.core.get_duration(y=y, sr=sr)
    samples_in_time = librosa.core.samples_to_time(np.arange(y.size), sr=sr)
    wanted_indices = np.searchsorted(samples_in_time, onsets)
    # care about signal energy, so take abs value
    amp_array = np.abs(y[wanted_indices])
    amp_dict = {key: value for key, value in zip(onsets, amp_array)}
    return amp_dict, amp_array, duration


if __name__ == '__main__':
    for file in os.listdir("songs"):
        if file.endswith(".wav"):
            name = os.path.splitext(file)[0]
            path = "songs/" + name + ".wav"
            print("Processing {}".format(name))
            json_dict = {}
            onsets = get_onsets(path)
            json_dict["onsets"] = onsets
            file_path = "output/"+ name +".json" ## your path variable
            json.dump(json_dict, codecs.open(file_path, 'w', encoding='utf-8'), separators=(',', ':'), sort_keys=True, indent=4) ### this saves the array in .json format
    print("DONE PROCESSING!")
