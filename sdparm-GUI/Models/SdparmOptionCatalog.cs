using System.Collections.Generic;

namespace sdparm_GUI.Models;

public static class SdparmOptionCatalog
{
    public static IReadOnlyList<SdparmOption> All { get; } = new List<SdparmOption>
    {
        new("all", "a", "list all known (generic or transport) parameters for given DEVICE. When this utility is invoked with no options then only common generic mode parameters are output.", false),
        new("clear", "c", "clear (zero) parameter(s) in STR. The default action to clear can be overridden, see below.", true, "STR"),
        new("command", "C", "perform CMD which is one of: capacity, eject, load, ready, sense, start, stop, sync or unlock", true, "CMD"),
        new("dbd", "B", "set the DBD (disable block descriptors) bit in each MODE SENSE cdb", false),
        new("defaults", "D", "fetch the default values for the given mode page and use them to overwrite the current values. Note that the saved values are not overwritten unless the '--save' option is also given.", false),
        new("dummy", "d", "when used with '--set' or '--clear' does all the preparation and sanity checks but bypasses the final stage of sending the changes to the device. [That is, it skips the MODE SELECT command.]", false),
        new("enumerate", "e", "fetch information from the sdparm's internal tables. If a DEVICE name is given then it will be ignored. May be used in conjunction with '--all', '--inquiry', '--long', '--page=', '--transport=' and/or '--vendor='.", false),
        new("examine", "E", "probes all mode or VPD pages. For mode pages only those with known field names are probed when the --all option is given. For VPD pages only those pages listed in \"Supported VPD pages page\" are decoded. In both cases some pages may be missed.", false),
        new("flexible", "f", "check mode sense responses for sanity and if broken, try to fix them if possible. Also allows mode pages whose peripheral type mismatches the given DEVICE to be processed.", false),
        new("get", "g", "fetch parameter(s) in STR", true, "STR"),
        new("help", "h", "output a usage message then exits", false),
        new("hex", "H", "rather than decode a (mode or VPD) page, it is output in ASCII hex. If used with the '--get' option then the parameter is output in hexadecimal. May be invoked multiple times.", false),
        new("inhex", "I", "read ASCII hex from file FN (and ignore DEVICE) and decode as mode or VPD page(s). Reads from stdin if FN is '-'. Treats the contents of FN as binary if '--raw' is also given.", true, "FN"),
        new("inquiry", "i", "fetch a VPD page, decode and output it. If no '--page=' is given then the device identification VPD page is fetched. Add '-ll' to get standard INQUIRY response data decoded in more detail.", false),
        new("long", "l", "Add extra information to the output. For example a line showing the setting of the WCE parameter will have \"Write cache enable\" appended to it. Using '-ll' adds information about selected mode parameter values (e.g. MRIE).", false),
        new("num-desc", "n", "for a mode page that can have descriptors, the number of descriptors in the given page on DEVICE is output. Otherwise 0 is output.", false),
        new("out-mask", "o", "the value OM (default decimal) selects whether current (0x1), changeable (0x2), default (0x4) and/or saveable (0x8) values are output. The default is 0xf (i.e. 0x1|0x2|0x4|0x8).", true, "OM"),
        new("page", "p", "page (and optionally subpage) to output or change. Argument may be either an abbreviation, a number or two numbers separated by a comma. If a number is prefixed by \"0x\" or has a trailing \"h\" then it is decoded as hexadecimal. When a numeric argument is given, it is assumed to be for a mode page unless the '--inquiry' option is also given. An abbreviation is two or three lower case letters (e.g. \"ca\" for the caching mode page). An abbreviation may be for a mode page (e.g. \"ca\") or a VPD page (e.g. \"sp\").", true, "PG[,SPG]"),
        new("pdt", "P", "where DT is the \"peripheral device type\" value (e.g. 0 for a disk, 1 for a tape drive, etc). Only active when '--inhex=FN' is also used.", true, "DT"),
        new("quiet", "q", "suppress vendor/product/revision strings that are usually the first line of the output. Also abridges the output of the device identification VPD page.", false),
        new("raw", "R", "used together with '--inhex=FN'. It causes the file FN to be interpreted as binary.", false),
        new("readonly", "r", "override other logic and open DEVICE in read-only mode. This can help with ATA disks especially with commands like '-C stop'. Also works with '--set=' and '--clear='.", false),
        new("save", "S", "also write changes to corresponding \"saved\" values mode page. Active when used with '--set=', '--clear=' or '--defaults'. The default action is to only make changes to the current values mode page.", false),
        new("set", "s", "set parameter(s) in STR. To set a parameter is to make all its bits one. The default action to set can be overridden, see below.", true, "STR"),
        new("six", "6", "use 6 byte cdbs for MODE SENSE and MODE SELECT commands for getting and setting mode pages. The default action is to use the 10 byte cdb variants.", false),
        new("transport", "t", "transport protocol identifier; either a number or an abbreviation (e.g. \"fcp\", \"spi\" or \"sas\") can be given. See transports section. In the absence of an explicit transport and if a page or field name does not match a generic name then the SAS transport is assumed.", true, "TN"),
        new("vendor", "M", "vendor (manufacturer) identifier; either a number or an abbreviation (e.g. \"sea\" or \"hit\") can be given. See vendors section.", true, "VN"),
        new("verbose", "v", "increase verbosity of output. May be used multiple times to further increase verbosity.", false),
        new("version", "V", "print out the version and the date of last code change then exits", false),
        new("wscan", "w", "[Windows only] scan for device names, show one device per line. Each device can have multiple device names. If a DEVICE name is given (on the command line) then it will be ignored.", false),
    };
}
