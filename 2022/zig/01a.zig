const std = @import("std");

pub fn main() !void {
    const file = try std.fs.cwd().openFile("input/01", .{});
    defer file.close();

    var buf_reader = std.io.bufferedReader(file.reader());
    var in_stream = buf_reader.reader();

    var max_cals: i32 = 0;
    var current_cals: i32 = 0;

    var buf: [1024]u8 = undefined;
    while (try in_stream.readUntilDelimiterOrEof(&buf, '\n')) |line| {
        if (line.len == 0) {
            max_cals = std.math.max(max_cals, current_cals);
            current_cals = 0;
            continue;
        }
        current_cals += try std.fmt.parseInt(i32, line, 10);
    }

    std.debug.print("{}\n", .{max_cals});
}
