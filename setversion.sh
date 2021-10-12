#!/usr/bin/env bash

set -e

prog=$(basename $0)
base_dir=$(cd $(dirname $0) && pwd)

if command -v gsed >/dev/null; then
  sed=gsed
else
  sed=sed
fi

usage() {
  cat <<EOF
USAGE
      $prog [-h] version
WHERE
      version
              Template version in <major>.<minor>.<patch> format.
OPTIONS
      -h      print this message
DESCRIPTION
      Updates the NuGet spec and DevOps pipeline with the specified version.
EOF
}

die() {
  echo $* 2>&1
  exit 1
}

while getopts ":h" opt ; do
  case $opt in
    h)
      usage
      exit
      ;;
    \?)
      die "invalid option -$OPTARG; run with -h for help"
      ;;
    :)
      die "option -$OPTARG requires an argument; run with -h for help"
      ;;
  esac
done
shift $(($OPTIND - 1))

if [ $# -eq 0 ]; then
  die "version not specified; run with -h for help"
fi

version=$(echo $1 | cut -d- -f1)
shift

if [ $# -gt 0 ]; then
  die "too many args; run with -h for help"
fi

major=$(echo $version | cut -d. -f1)
minor=$(echo $version | cut -d. -f2)
patch=$(echo $version | cut -d. -f3)

if [[ "$major.$minor.$patch" != "$version" ]]; then
  die "version not in major.minor.patch format"
fi
[[ $major =~ ^-?[0-9]+$ ]] || die "invalid major value: $major"
[[ $minor =~ ^-?[0-9]+$ ]] || die "invalid minor value: $minor"
[[ $patch =~ ^-?[0-9]+$ ]] || die "invalid patch value: $patch"

if ! $sed --version 2>/dev/null | grep 'GNU sed' >/dev/null; then
  die "This script requires GNU sed"
fi

echo "Setting version to $major.$minor.$patch"

$sed -i 's:<SteeltoeInitializrApiVersion>.*</SteeltoeInitializrApiVersion>:<SteeltoeInitializrApiVersion>'$major.$minor.$patch'</SteeltoeInitializrApiVersion>:' $base_dir/Version.props
