const std = @import("std");

pub fn main() !void {
    const file = try std.fs.cwd().openFile("input/01", .{});
    defer file.close();

    var buf_reader = std.io.bufferedReader(file.reader());
    var in_stream = buf_reader.reader();

    var max_cals = [_]i32{ 0, 0, 0, 0 };
    var current_cals: i32 = 0;

    var buf: [1024]u8 = undefined;
    while (try in_stream.readUntilDelimiterOrEof(&buf, '\n')) |line| {
        if (line.len == 0) {
            max_cals[0] = current_cals;
            std.sort.block(i32, &max_cals, {}, std.sort.asc(i32));
            current_cals = 0;
            continue;
        }
        current_cals += try std.fmt.parseInt(i32, line, 10);
    }

    std.debug.print("{}\n", .{max_cals[1] + max_cals[2] + max_cals[3]});
}
