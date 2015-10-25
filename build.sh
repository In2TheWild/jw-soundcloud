mono .paket/paket.bootstrapper.exe
mono .paket/paket.exe restore
mono packages/FAKE/tools/Fake.exe build.fsx

#fsharpi build.fsx
