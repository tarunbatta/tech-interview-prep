#!/bin/bash

# Configuration
REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$REPO_ROOT" || exit 1
TRACKING_FILE=".diagram-tracking"
DRAWIO_PATH="/Applications/draw.io.app/Contents/MacOS/draw.io"


# Function to get file hash
get_file_hash() {
    local file="$1"
    if [ -f "$file" ]; then
        shasum -a 256 "$file" | cut -d' ' -f1
    else
        echo ""
    fi
}

# Function to get relative path
get_relative_path() {
    local file="$1"
    # Remove leading ./ if present
    echo "${file#./}"
}

# Function to check if file needs conversion
needs_conversion() {
    local drawio_file="$1"
    local rel_file=$(get_relative_path "$drawio_file")
    local hash=$(get_file_hash "$drawio_file")
    local stored_hash=$(grep "^$rel_file:" "$TRACKING_FILE" 2>/dev/null | cut -d':' -f2)

    if [ "$hash" != "$stored_hash" ]; then
        return 0  # Needs conversion
    else
        return 1  # No conversion needed
    fi
}

# Function to update tracking file
update_tracking() {
    local drawio_file="$1"
    local rel_file=$(get_relative_path "$drawio_file")
    local hash=$(get_file_hash "$drawio_file")
    # Remove old entry if exists
    sed -i '' "/^$rel_file:/d" "$TRACKING_FILE" 2>/dev/null
    # Add new entry
    echo "$rel_file:$hash" >> "$TRACKING_FILE"
}

# Function to convert a single diagram
convert_diagram() {
    local drawio_file="$1"
    local base_name="${drawio_file%.drawio}"
    
    echo "Converting $drawio_file..."
    
    # Convert to SVG
    "$DRAWIO_PATH" --export --format svg --output "${base_name}.svg" "$drawio_file"

    # Insert white background rect as first child of <svg>
    if [ -f "${base_name}.svg" ]; then
        # Insert <rect ...> after the first <svg ...> line
        sed -i '' '0,/<svg[^>]*>/s//&\n  <rect width="100%" height="100%" fill="#fff"\/>/' "${base_name}.svg"
    fi

    # Update tracking
    update_tracking "$drawio_file"
}

# Main script
main() {
    # Create tracking file if it doesn't exist
    touch "$TRACKING_FILE"
    
    # Find all draw.io files
    find system-design -type f -name "*.drawio" | sort | while read -r file; do
        if needs_conversion "$file"; then
            convert_diagram "$file"
        else
            echo "Skipping $file (no changes detected)"
        fi
    done
    
    echo "Conversion complete!"
}

# Run the script
main 