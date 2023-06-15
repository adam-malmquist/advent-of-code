const std = @import("std");

pub fn main() !void {
    const file = try std.fs.cwd().openFile("input/02", .{});
    defer file.close();

    var buf: [4]u8 = undefined;
    var score: u32 = 0;

    while (try file.read(&buf) == buf.len) {
        switch (buf[2]) {
            'X' => {
                score += 1;
                if (buf[0] == 'A') {
                    score += 3;
                } else if (buf[0] == 'C') {
                    score += 6;
                }
            },
            'Y' => {
                score += 2;
                if (buf[0] == 'B') {
                    score += 3;
                } else if (buf[0] == 'A') {
                    score += 6;
                }
            },
            'Z' => {
                score += 3;
                if (buf[0] == 'C') {
                    score += 3;
                } else if (buf[0] == 'B') {
                    score += 6;
                }
            },
            else => unreachable,
        }
    }

    std.debug.print("score: {}\n", .{score});
}
