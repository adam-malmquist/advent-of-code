const std = @import("std");

pub fn main() !void {
    const file = try std.fs.cwd().openFile("input/02", .{});
    defer file.close();

    var buf: [4]u8 = undefined;
    var score: u32 = 0;

    while (try file.read(&buf) == buf.len) {
        score += switch (buf[0]) {
            'A' => switch (buf[2]) {
                'X' => 3 + 0,
                'Y' => 1 + 3,
                'Z' => 2 + 6,
                else => unreachable,
            },
            'B' => switch (buf[2]) {
                'X' => 1 + 0,
                'Y' => 2 + 3,
                'Z' => 3 + 6,
                else => unreachable,
            },
            'C' => switch (buf[2]) {
                'X' => 2 + 0,
                'Y' => 3 + 3,
                'Z' => 1 + 6,
                else => unreachable,
            },
            else => unreachable,
        };
    }

    std.debug.print("score: {}\n", .{score});
}
