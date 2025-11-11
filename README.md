# LoneSpoof
Just another AMIDEWIN perm spoofer ;) nothing new or extraordinary

## Overview
I created this as a quick and easy way to spoof SMBIOS for AMI BIOS based motherboards (most but not all of them). There are a lot of paid solutions that just use these free tools (AMIDEWIN), and an even greater amount of free stuff on GitHub that can do fairly destructive things to your SMBIOS.

I lifted the AMIDEWIN binaries from a recent Lenovo package and confirmed they work on Windows 11!

This is a fairly minimal/safe approach that should be good for most people. This will prompt the user what they want to set each value to, I recommend going with the defaults unless you know what you are doing. I don't "autogen" serials for things like the System or Baseboard since it can differ vendor to vendor as far as their format goes, and this could raise even _more_ flags against you.

It's basic but it's free :)

## Limitations
- Only works on Motherboards with AMI BIOS.
- Will not work on ASUS motherboards (surprise surprise).
- Only spoofs your SMBIOS. Other common spoofing steps are detailed below.

# Instructions
1. Run the tool on the PC you want to spoof and follow the prompts. Save the generated Logfile in case you need to revert back to your original values.
2. After successful spoof, Re-Flash your BIOS, and Re-Install Windows.
   1. There are some other tools that clear traces, but there are a million and I can't be bothered to do them all. Reinstalling Windows is a better way to clear traces anyway.
   1. [Create Windows Installation Media](https://support.microsoft.com/windows/create-installation-media-for-windows-99a58364-8c02-206f-aa6f-40c3b507420d)
   1. [Massgrave Microsoft Activation Scripts (to activate Windows)](https://massgrave.dev/)
3. Spoof Disk Serials:
   1. ✅ Best option is **RAID 0** (Requires Windows Re-Install). [RAID 0 Guide (Gigabyte)](https://download.gigabyte.com/FileList/Manual/mb_manual_intel700series-raid_e.pdf)
   1. ⚠️ There are some `nulled` serial hard drives you can buy, or certain Chinese models that you can use OEM tools to change the Disk Serial, but these could potentially raise flags against you.
   1. ❌ [VolumeId](https://learn.microsoft.com/sysinternals/downloads/volumeid) just changes your Volume Serial (set randomly when your drive is formatted), and is useless.
4. Spoof MAC Address:
   1. ✅ Change it directly in **Network Connections**.
   1. ![spoof_mac](https://github.com/user-attachments/assets/99d2bf99-8519-48f3-8539-bbdeb9d15f06)
   1. Use `IPCONFIG /ALL` to get your existing Ethernet MAC Address. Only change the last 24 bits (last 6 characters). The first 24 bits are the Vendor ID and should be left alone.
   1. Do this **AFTER** re-installing Windows.
   1. For more advanced AC's you may need to spoof your ARP Table too, but I will not be going into that.
5. Congrats! You're all spoofed.
 
# Support
This is released **AS-IS** and there is no support. But if I need to change something or you have a suggestion just let me know.
