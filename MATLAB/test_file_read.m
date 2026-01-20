fn = 'C:\Users\hancock\Documents\TobiiRecordings\TobiiRecording-20260120-173556.tsr';

dataFormat = {
    'int64', [1 1], 'deviceTime';
    'int64', [1 1], 'systemTime';
    'uint8', [1 1], 'leftGazeValid';
    'single', [1 1], 'leftX';
    'single', [1 1], 'leftY';
    'single', [1 1], 'leftPupil';
    'uint8', [1 1], 'rightGazeValid';
    'single', [1 1], 'rightX';
    'single', [1 1], 'rightY';
    'single', [1 1], 'rightPupil';
};

recordSize = 42;
finfo = dir(fn);
numRecords = floor(finfo.bytes / recordSize);

m = memmapfile(fn, ...
               'Format', dataFormat, ...
               'Repeat', numRecords);

d = m.Data;

deviceTime = [d.deviceTime];
systemTime = [d.systemTime];
leftPupil = [d.leftPupil];
rightPupil = [d.rightPupil];

t = deviceTime - deviceTime(1);
t = double(t) * 1e-6;

figure(1);
plot(t, leftPupil, t, rightPupil);
