# LoneSpoof
Just another AMIDEWIN perm spoofer ;) nothing new or extraordinary

## Overview
I created this as a quick and easy way to spoof SMBIOS for AMI BIOS based motherboards (most but not all of them). There are a lot of paid solutions that just use these free tools (AMIDEWIN), and an even greater amount of free stuff on GitHub that can do fairly destructive things to your SMBIOS.

I lifted the AMIDEWIN binaries from a recent Lenovo package and confirmed they work on Windows 11!

This is a fairly minimal/safe approach that should be good for most people. This will prompt the user what they want to set each value to, I recommend going with the defaults unless you know what you are doing. I don't "autogen" serials for things like the System or Baseboard since it can differ vendor to vendor as far as their format goes, and this could raise even _more_ flags against you.

It's basic but it's free :)

## Limitations
- Only works on Motherboards with AMI BIOS.
  - Will not work on ASUS motherboards (surprise surprise)
- Will not fix your Disk Serials (you will need to RAID 0, etc. still)
  - I saw some other solutions using the MS tool VolumeId but I am skeptical this will be any good for even BattlEye, since I am pretty sure they use a much lower level hook and grab the actual serial number off the disk. The Volume ID is just randomly set during format.
  - I may look into this more if I get time
- Will not fix your MAC Address.
  - Usually this is easy to spoof since most drivers allow you to change this directly in Network Connections.

# Instructions
- Build the tool (Right-Click Project -> Publish -> Self-Contained/win-x64)
- Run the tool on the PC you want to spoof and follow the prompts.
- Save the 'Originals' json file in case you need to revert back to your original values.
- After successful spoof, Re-Flash your BIOS, and Re-Install Windows
  - There are some other tools that clear traces, but there are a million and I can't be bothered to do them all. Reinstalling Windows is a better way to clear traces anyway.
 
# Support
As-Is, no support. But if you see anything that I should change/look at feel free to let me know :)
