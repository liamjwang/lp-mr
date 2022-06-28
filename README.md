# Lumbar Puncture MR
[![Build linux players](https://github.com/liamjwang/lp-mr/actions/workflows/build-linux.yml/badge.svg)](https://github.com/liamjwang/lp-mr/actions/workflows/build-linux.yml)
[![Build windows players](https://github.com/liamjwang/lp-mr/actions/workflows/build-windows.yml/badge.svg)](https://github.com/liamjwang/lp-mr/actions/workflows/build-windows.yml)


## Development
### CI Setup
- Assets/Editor/UnityBuilderAction is required for GameCI builds
  - Enables "Copy References", required for appx build from vs solution build to work

### References
- https://github.com/OpenAvikom/mr-grpc-unity
- https://github.com/rderbier/Hololens-QRcodeSample
- https://game.ci/

### Bugs
- slice rendering requires external window to be visible

### Notes
- https://github.com/microsoft/MixedRealityToolkit-Unity/issues/10449#issuecomment-1111163353\
- MRTK/Standard shader error
  - delete: fixed facing:VFACE in line 775
    and *facing in line 956,959